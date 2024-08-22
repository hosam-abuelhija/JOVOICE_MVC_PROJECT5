var simplemaps_countrymap_mapdata = {
    main_settings: {
        //General settings
        width: "responsive", //'700' or 'responsive'
        background_color: "#FFFFFF",
        background_transparent: "yes",
        border_color: "#003a66",

        //State defaults
        state_description: "State description",
        state_color: "#88A4BC",
        state_hover_color: "#003a66",
        state_url: "",
        border_size: "1",
        all_states_inactive: "no",
        all_states_zoomable: "yes",

        //Location defaults
        location_description: "Location description",
        location_url: "",
        location_color: "#FF0067",
        location_opacity: 0.8,
        location_hover_opacity: 1,
        location_size: 25,
        location_type: "square",
        location_image_source: "frog.png",
        location_border_color: "#FFFFFF",
        location_border: 2,
        location_hover_border: 2.5,
        all_locations_inactive: "no",
        all_locations_hidden: "no",

        //Label defaults
        label_color: "#003a66",
        label_hover_color: "#ffffff",
        label_size: "24",
        label_font: "Arial",
        label_display: "auto",
        label_scale: "yes",
        hide_labels: "no",
        hide_eastern_labels: "no",

        //Zoom settings
        zoom: "yes",
        manual_zoom: "no",
        back_image: "no",
        initial_back: "no",
        initial_zoom: "-1",
        initial_zoom_solo: "no",
        region_opacity: 1,
        region_hover_opacity: 0.6,
        zoom_out_incrementally: "yes",
        zoom_percentage: 0.99,
        zoom_time: 0.5,

        //Popup settings
        popup_color: "white",
        popup_opacity: 0.9,
        popup_shadow: 1,
        popup_corners: 5,
        popup_font: "12px/1.5 Verdana, Arial, Helvetica, sans-serif",
        popup_nocss: "no",

        //Advanced settings
        div: "map",
        auto_load: "yes",
        url_new_tab: "no",
        images_directory: "default",
        fade_time: 0.1,
        link_text: "View Website",
        popups: "detect",
        state_image_url: "",
        state_image_position: "",
        location_image_url: "",
        border_hover_color: "#003a66",
        border_hover_size: ""
    },
    state_specific: {
        JOAJ: {
            name: "عجلون",
            color: "#ff9393",
            description: "81%",
            url: "Login"
        },
        JOAM: {
            name: "عمان",
            color: "#f3c759",
            description: "العاصمة الأولى : 79%  |  العاصمة الثانية : 83%  |  العاصمة الثالثة : 81% ـ",
            url: "Login"
        },
        JOAQ: {
            name: "العقبة",
            color: "#f0f18c",
            description: "78%",
            url: "Login"
        },
        JOAT: {
            name: "الطفيلة",
            color: "#cdf690",
            description: "81%",
            url: "Login"
        },
        JOAZ: {
            name: "الزرقاء",
            color: "#8cf1d9",
            description: "90%",
            url: "Login"
        },
        JOBA: {
            name: "البلقاء",
            color: "#8cc3f1",
            description: "87%",
            url: "Login"
        },
        JOIR: {
            name: "اربد",
            color: "#bca684",
            description: "اربد الأولى : @Session["localPercentIrbid1"]%  |  اربد الثانية : @Session["localPercentIrbid2"]% ـ",
            url: "Login"
        },
        JOJA: {
            name: "جرش",
            color: "#b992ff",
            description: "80%",
            url: "Login"
        },
        JOKA: {
            name: "الكرك",
            color: "#ffd4fb",
            description: "85%",
            url: "Login"
        },
        JOMA: {
            name: "المفرق",
            color: "#e47aaf",
            description: "@Session["localPercentMafraq"]%",
            url: "Login"
        },
        JOMD: {
            name: "مأدبا",
            color: "#e05454",
            description: "83%",
            url: "Login"
        },
        JOMN: {
            name: "معان",
            color: "#c7d9ff",
            description: "84%",
            url: "Login"
        }
    },
    locations: {},
    labels: {
        JOAJ: {
            name: "عجلون",
            parent_id: "JOAJ"
        },
        JOAM: {
            name: "عمان",
            parent_id: "JOAM"
        },
        JOAQ: {
            name: "العقبة",
            parent_id: "JOAQ"
        },
        JOAT: {
            name: "الطفيلة",
            parent_id: "JOAT"
        },
        JOAZ: {
            name: "الزرقاء",
            parent_id: "JOAZ"
        },
        JOBA: {
            name: "البلقاء",
            parent_id: "JOBA"
        },
        JOIR: {
            name: "اربد",
            parent_id: "JOIR"
        },
        JOJA: {
            parent_id: "JOJA",
            name: "جرش"
        },
        JOKA: {
            parent_id: "JOKA",
            name: "الكرك"
        },
        JOMA: {
            parent_id: "JOMA",
            name: "المفرق"
        },
        JOMD: {
            parent_id: "JOMD",
            name: "مأدبا"
        },
        JOMN: {
            parent_id: "JOMN",
            name: "معان"
        }
    },
    legend: {
        entries: []
    },
    regions: {}
};