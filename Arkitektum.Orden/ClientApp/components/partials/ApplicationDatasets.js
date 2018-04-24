/* Components */
import ApplicationDataset from './ApplicationDatasets/ApplicationDataset.vue';

export default {
    name: 'ApplicationDatasets',
    props: {
        applicationDatasets: Array,
        applicationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ApplicationDataset
    },
    data() {
        return {
            apiData: null,            
            newApplicationDataset: false,
        }
    },
    mounted() {
        this.apiData = this.applicationDatasets;
    },
    methods: {
        createNewApplicationDataset() {
            this.newApplicationDataset = true;
        },
        removeNewApplicationDataset() {
            this.newApplicationDataset = false;
        },
        getApplicationDatasets() {
            Promise.resolve(this.$root.getApiData(`/datasets/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        postApplicationDataset(data) {
            Promise.resolve(this.$root.postApiData(`/datasets/application/`, { datasetId: data.id, applicationId: this.applicationId } ))
                .then(() => {
                    this.getApplicationDatasets();
                    this.removeNewApplicationDataset();
                });
        },
        removeApplicationDataset(datasetId) {
            Promise.resolve(this.$root.deleteApiData(`/datasets/application/${datasetId}/${this.applicationId}`))
                .then(() => {
                    this.getApplicationDatasets();
                });
        }
    }
}

