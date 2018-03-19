import axios from 'axios';

/* Components */
import ResourceLink from '../modules/ResourceLink.vue';

export default {
    name: 'Sector',
    data: function () {
        return {
            name: '',
            apiData: null,
            test: {}
        };
    },
    mounted() {
        //this.test = this.$root.getApiData('/Sectors/EditJson')
        axios.get('/Sectors/EditJson').then(response => {
            this.apiData = response.data;
        });
    },
    components: {
        ResourceLink
    }
}

