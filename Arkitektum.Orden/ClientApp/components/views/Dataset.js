/* Components */
import ResourceLinks from '../partials/ResourceLinks.vue';
import DatasetApplications from '../partials/DatasetApplications.vue';
import DatasetFields from '../partials/DatasetFields.vue';
import { format } from 'date-fns';


export default {
    name: 'Dataset',
    props: {
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        },
        metadataLists: Array
    }, 
    data() {
        return {
            apiUrls: {
                get: `/api/datasets/${this.datasetId}`,
                edit: `/datasets/edit/${this.datasetId}`
            },
            apiData: {}
        }
    },
    components: {
        ResourceLinks,
        DatasetApplications,
        DatasetFields
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
            if (!value) return '';
            return format(value, 'D. MMMM YYYY HH:mm:ss');
        },
        formatBoolean: function (value) {
            if (value) 
                return 'Ja';
            else 
                return 'Nei';
        }        
    }
}

