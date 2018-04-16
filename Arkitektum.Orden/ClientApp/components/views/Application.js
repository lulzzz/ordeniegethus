/* Components */
import ResourceLinks from '../partials/ResourceLinks.vue';
import ApplicationSectors from '../partials/ApplicationSectors.vue';
import { format } from 'date-fns';


export default {
    name: 'Application',
    props: ['applicationId'],
    data() {
        return {
            apiUrls: {
                get: `/Applications/${this.applicationId}`
            },
            apiData: null
        }
    },
    components: {
        ResourceLinks,
        ApplicationSectors
    },
    mounted() {
        this.getApiData();
    },
    methods: {
        getApiData() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },

    },
    filters: {
        formatDate: function (value) {
            if (!value) return ''
            return format(value, 'D. MMMM YYYY HH:mm:ss')
        }
    }
}

