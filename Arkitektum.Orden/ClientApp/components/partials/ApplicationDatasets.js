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
            availableDatasets: [],          
            newApplicationDataset: false,
        }
    },
    mounted() {
        this.apiData = this.applicationDatasets;
    },
    watch: {
        apiData() {
            if (this.apiData) {
                this.getAvailableDatasets();
            }
        }
    },
    methods: {
        createNewApplicationDataset() {
            this.newApplicationDataset = true;
        },
        removeNewApplicationDataset() {
            this.newApplicationDataset = false;
        },
        getApplicationDatasets() {
            Promise.resolve(this.$root.getApiData(`/api/applications/${this.applicationId}/datasets`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        getAvailableDatasets() {
            let availableDatasets = [];
            Promise.resolve(this.$root.getApiData(`/api/datasets/all`))
            .then((apiData) => {
                apiData.forEach(dataset => {
                    if (!this.apiData.filter(d => d.id == dataset.id).length) {
                        availableDatasets.push(dataset);
                    }
                });
                this.availableDatasets = availableDatasets;
            });
        },
        postApplicationDataset(data) {
            Promise.resolve(this.$root.postApiData(`/api/dataset-application/`, { datasetId: data.id, applicationId: this.applicationId } ))
                .then(() => {
                    this.getApplicationDatasets();
                    this.removeNewApplicationDataset();
                });
        },
        removeApplicationDataset(datasetId) {
            Promise.resolve(this.$root.deleteApiData(`/api/dataset-application/${datasetId}/${this.applicationId}`))
                .then(() => {
                    this.getApplicationDatasets();
                });
        }
    }
}

