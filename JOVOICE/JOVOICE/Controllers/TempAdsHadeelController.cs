using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JOVOICE.Models;

namespace JOVOICE.Controllers
{
    public class TempAdsHadeelController : Controller
    {
        private ElectionEntities2 db = new ElectionEntities2();

        private readonly string PayPalBaseUrl = "https://api.sandbox.paypal.com/";
        private readonly string ClientId = "AXwx3JTzFOALDYX_TQN2cHoQf2ACbzs-TwrSoL19-pq1jdJ8Eb6IDwZjhi8WurVinLzKYfxEVcKm_FcH"; // Replace with your sandbox client ID
        private readonly string Secret = "EBggmb0AM8TxCt4XYAARvlRMv5fyyV9XOpVUh83tpJ1myho4911uXDORVian3GFWgPMOY9jX2Ti1yvxX"; // Replace with your sandbox secret

        // GET: TempAdsHadeel
        public ActionResult Index()
        {
            return View(db.TempAds.ToList());
        }

        // GET: TempAdsHadeel/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempAd tempAd = db.TempAds.Find(id);
            if (tempAd == null)
            {
                return HttpNotFound();
            }
            return View(tempAd);
        }

        // GET: TempAdsHadeel/Create
        public ActionResult Create1()
        {
            return View();
        }

        // POST: TempAdsHadeel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "id,name,listname,electionarea,description")] TempAd tempAd, HttpPostedFileBase upload, PartyCandidate party, LocalCandidate local)
        {


            var fileName = Path.GetFileName(upload.FileName);
            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);


            upload.SaveAs(path);
            tempAd.image = fileName;

            if (ModelState.IsValid)
            {
                if (tempAd.name != local.name && tempAd.name != party.partyname)
                {
                    db.TempAds.Add(tempAd);
                    db.SaveChanges();
                    return RedirectToAction("Payment", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "هذا المرشح غير مسجل ");
                }

            }
            return View(tempAd);
        }

        public async Task<ActionResult> Checkout()
        {
            try
            {
                var accessToken = await GetAccessToken();

                var paymentPayload = new
                {
                    intent = "sale",
                    payer = new
                    {
                        payment_method = "paypal"
                    },
                    transactions = new[]
                    {
                        new
                        {
                            amount = new
                            {
                                total = "1.1",
                                currency = "USD"
                            },
                            description = "Payment description"
                        }
                    },
                    redirect_urls = new
                    {
                        cancel_url = "https://example.com/cancelUrl",
                        return_url = Url.Action("Success", "Payment1", null, Request.Url.Scheme)
                    }
                };

                var paymentJson = Newtonsoft.Json.JsonConvert.SerializeObject(paymentPayload);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PayPalBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(paymentJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/v1/payments/payment", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);

                        string approvalUrl = null;
                        var linksArray = responseObject.links as Newtonsoft.Json.Linq.JArray;
                        if (linksArray != null)
                        {
                            var approvalLink = linksArray.FirstOrDefault(l => (string)l["rel"] == "approval_url");
                            if (approvalLink != null)
                            {
                                approvalUrl = approvalLink["href"].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(approvalUrl))
                        {
                            return Redirect(approvalUrl);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Approval URL not found in PayPal response.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to initiate PayPal payment: " + response.ReasonPhrase;
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }
        private async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PayPalBaseUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientId}:{Secret}")));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                var requestContent = new FormUrlEncodedContent(requestData);
                var response = await client.PostAsync("/v1/oauth2/token", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return responseObject.access_token;
                }
                else
                {
                    throw new Exception("Failed to retrieve PayPal access token: " + response.ReasonPhrase);
                }
            }
        }
        // GET: TempAdsHadeel/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempAd tempAd = db.TempAds.Find(id);
            if (tempAd == null)
            {
                return HttpNotFound();
            }
            return View(tempAd);
        }

        // POST: TempAdsHadeel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,listname,electionarea,image,description")] TempAd tempAd, HttpPostedFileBase upload)
        {

            var fileName = Path.GetFileName(upload.FileName);
            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);


            upload.SaveAs(path);
            tempAd.image = fileName;

            if (ModelState.IsValid)
            {
                db.TempAds.Add(tempAd);
                db.SaveChanges();
                return RedirectToAction("Index", "TempAdsHadeel");
            }

            ViewBag.id = new SelectList(db.Ads, "id", "name", tempAd.id);
            db.SaveChanges();
            return View();

        }

        // GET: TempAdsHadeel/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempAd tempAd = db.TempAds.Find(id);
            if (tempAd == null)
            {
                return HttpNotFound();
            }
            return View(tempAd);
        }

        // POST: TempAdsHadeel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TempAd tempAd = db.TempAds.Find(id);
            db.TempAds.Remove(tempAd);
            db.SaveChanges();
            return RedirectToAction("Index", "TempAdsHadeel");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
