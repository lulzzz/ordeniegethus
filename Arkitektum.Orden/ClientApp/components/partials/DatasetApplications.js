/* Components */
import DatasetApplication from './DatasetApplications/DatasetApplication.vue';

export default {
    name: 'DatasetApplications',
    props: {
        datasetApplications: Array,
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        DatasetApplication
    },
    data() {
        return {
            apiData: null,            
            newDatasetApplication: false,
        }
    },
    mounted() {
        this.apiData = this.datasetApplications;
    },
    methods: {
        createNewDatasetApplication() {
            this.newDatasetApplication = true;
        },
        removeNewDatasetApplication() {
            this.newDatasetApplication = false;
        },
        getDatasetApplications() {
            Promise.resolve(this.$root.getApiData(`/applications/dataset/${this.datasetId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        postApplicationDataset(data) {
            Promise.resolve(this.$root.postApiData(`/applications/dataset/`, { applicationId: data.id, datasetId: this.datasetId } ))
                .then(() => {
                    this.getDatasetApplications();
                    this.removeNewDatasetApplication();
                });
        },
        removeDatasetApplication(applicationId) {
            Promise.resolve(this.$root.deleteApiData(`/applications/dataset/${applicationId}/${this.datasetId}`))
                .then(() => {
                    this.getDatasetApplications();
                });
        }
    }
}

