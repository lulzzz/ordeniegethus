import './scss/main.scss';
import 'bootstrap';
import Vue from 'vue';

import App from './components/app/app.vue';


new Vue({
    el: '#app-root',
    render: h => h(App)
});
