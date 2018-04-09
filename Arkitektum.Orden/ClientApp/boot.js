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
        getApiData(url) {
            return axios.get(url)
                .then(response => { return response.data; })
                .catch(error => { throw error });
        },
        postApiData(url, apiData) {
            return axios.post(url, apiData)
                .then(response => { console.log(response); })
                .catch(error => { throw error });
        },
        putApiData(url, apiData) {
            return axios.put(url, apiData)
                .then(response => { console.log(response); })
                .catch(error => { throw error });
        }
    }
});
