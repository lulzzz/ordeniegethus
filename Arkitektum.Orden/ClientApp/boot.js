import './scss/main.scss';
import 'bootstrap';
import Vue from 'vue';


/* Vuex store */
import store from './store'

/* Views */
import Sector from './components/views/Sector.vue';


new Vue( {
    el: '#app-root',
    store,
    components: {
        Sector,
    }
});
