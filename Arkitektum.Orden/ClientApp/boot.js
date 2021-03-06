﻿import './scss/main.scss';
import './assets/icons';
import './scripts/init';
import 'bootstrap';
import jQuery from 'jquery';
window.jQuery = jQuery;
import Vue from 'vue';
import axios from 'axios';

/* Views */
import Application from './components/views/Application.vue';
import ApplicationRegistry from './components/views/ApplicationRegistry.vue';
import Dataset from './components/views/Dataset.vue';
import Sector from './components/views/Sector.vue';
import Organization from './components/views/Organization.vue';

new Vue( {
    el: '#app-root',
    components: {
        Application,
        ApplicationRegistry,
        Dataset,
        Sector,
        Organization
    },
    methods: {
        getApiData(url) {
            return axios.get(url)
                .then(response => { return response.data; })
                .catch(error => { throw error });
        },
        postApiData(url, apiData) {
            return axios.post(url, apiData)
                .then(response => { 
                    console.log(response);
                    return response.data;
                })
                .catch(error => { throw error });
        },
        putApiData(url, apiData) {
            return axios.put(url, apiData)
                .then(response => { 
                    console.log(response); 
                    return response.data;
                })
                .catch(error => { throw error });
        },
        deleteApiData(url, apiData) {
            return axios.delete(url, apiData)
                .then(response => { console.log(response); })
                .catch(error => { throw error });
        }
    }
});
