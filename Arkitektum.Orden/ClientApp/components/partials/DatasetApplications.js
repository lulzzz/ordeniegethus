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
            availableApplications: [],
            newDatasetApplication: false
        }
    },
    mounted() {
        this.apiData = this.datasetApplications;
    },
    watch: {
        apiData() {
            if (this.apiData) {
                this.getAvailableApplications();
            }
        }
    },
    methods: {
        createNewDatasetApplication() {
            this.newDatasetApplication = true;
        },
        removeNewDatasetApplication() {
            this.newDatasetApplication = false;
        },
        getDatasetApplications() {
            Promise.resolve(this.$root.getApiData(`/api/datasets/${this.datasetId}/applications`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        getAvailableApplications() { 
            let availableApplications = []; 
            Promise.resolve(this.$root.getApiData(`/api/applications/all`)) 
            .then((apiData) => { 
                apiData.forEach(application => { 
                    if (!this.apiData.filter(a => a.id == application.id).length) { 
                        availableApplications.push(application); 
                    } 
                }); 
                this.availableApplications = availableApplications; 
            }); 
        }, 
        postDatasetApplication(data) {
            Promise.resolve(this.$root.postApiData(`/api/dataset-application/`, { applicationId: data.id, datasetId: this.datasetId } ))
                .then(() => {
                    this.getDatasetApplications();
                    this.removeNewDatasetApplication();
                });
        },
        removeDatasetApplication(applicationId) {
            Promise.resolve(this.$root.deleteApiData(`/api/dataset-application/${this.datasetId}/${applicationId}`))
                .then(() => {
                    this.getDatasetApplications();
                });
        }
    }
}

