/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationDataset',
    props: {
        applicationDataset: Object,
        selectedApplicationDatasets: Array,
        applicationId: String,
        saved: Boolean,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationDataset !== undefined ? this.applicationDataset : { name: '' },
            availableDatasets: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableDatasets();
        }
    },
    methods: {
        getAvailableDatasets() {
            Promise.resolve(this.$root.getApiData(`/datasets/all`))
            .then((apiData) => {
                this.availableDatasets = apiData;
            });
        },
        selectApplicationDataset(applicationDataset) {
            this.data = applicationDataset;
        },
        datasetDetailsPageUrl() {
            return `/datasets/details/${this.applicationDataset.id}`;
        }
    }
}
