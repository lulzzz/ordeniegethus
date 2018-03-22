import './scss/main.scss';
import './assets/icons';
import './scripts/init';
import 'bootstrap';
import Vue from 'vue';
import axios from 'axios';



/* Vuex store */
//import store from './store'

/* Views */
import Sector from './components/views/Sector.vue';


new Vue( {
    el: '#app-root',
    components: {
        Sector,
    },
    methods: {
        getApiData: function (url) {
            axios.get(url).then(response => {
                return response.data;
            });
        }
    }
});
