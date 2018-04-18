/* Components */
import ResourceLinks from '../partials/ResourceLinks.vue';
import ApplicationSectors from '../partials/ApplicationSectors.vue';
import ApplicationDatasets from '../partials/ApplicationDatasets.vue';
import ApplicationNationalComponents from '../partials/ApplicationNationalComponents.vue';
import { format } from 'date-fns';


export default {
    name: 'Application',
    props: ['applicationId'],
    data() {
        return {
            apiUrls: {
                get: `/Applications/${this.applicationId}`,
                edit: `/applications/edit/${this.applicationId}`,
                submitAppRegistry: `/applications/submit-app-registry/${this.applicationId}`,
            },
            apiData: null
        }
    },
    components: {
        ResourceLinks,
        ApplicationSectors,
        ApplicationDatasets,
        ApplicationNationalComponents
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

