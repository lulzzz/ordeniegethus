import axios from 'axios';

/* Components */
import ResourceLink from '../modules/ResourceLinks.vue';

export default {
    name: 'Sector',
    props: ['organizationId'],
    data: function () {
        return {
            context: false
        }
    },
    mounted: function() {
        axios.get('/Sectors/EditJson').then(response => {
            this.context = response.data;
        });
    },
    components: {
        ResourceLink
    }
}

