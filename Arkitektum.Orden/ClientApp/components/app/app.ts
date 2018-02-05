import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import DummyComponent from '../dummycomponent/dummycomponent.vue';

@Component({
    components: {
        DummyComponent
    }
})
export default class AppComponent extends Vue {
}
