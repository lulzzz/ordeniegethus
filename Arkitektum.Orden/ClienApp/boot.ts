import './css/site.css';
import 'bootstrap';
import Vue from 'vue';

new Vue({
    el: '#app-root',
    render: h => h(require('./components/app/app.vue.html'))
});
