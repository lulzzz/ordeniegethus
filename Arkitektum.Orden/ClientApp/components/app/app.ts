import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        DummyComponent: require('../dummycomponent/dummycomponent.vue')
    }
})
export default class AppComponent extends Vue {
}
