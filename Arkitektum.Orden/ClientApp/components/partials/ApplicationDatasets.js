/* Components */
import ApplicationDataset from './ApplicationDatasets/ApplicationDataset.vue';

export default {
    name: 'ApplicationDatasets',
    props: ['applicationDatasets', 'applicationId'],
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
            Promise.resolve(this.$root.postApiData(`/datasets/application/`, data))
                .then(() => {
                    this.getApplicationDatasets();
                    this.removeNewApplicationDataset();
                });
        },
        removeApplicationDataset(applicationDatasetId) {
            Promise.resolve(this.$root.deleteApiData(`/datasets/application/`, { id: applicationDatasetId }))
                .then(() => {
                    this.getApplicationDatasets();
                });
        }
    }
}

