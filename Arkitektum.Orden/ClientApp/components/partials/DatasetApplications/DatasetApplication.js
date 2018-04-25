/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'DatasetApplication',
    props: {
        datasetApplication: Object,
        selectedDatasetApplications: Array,
        datasetId: String,
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
            data: this.datasetApplication !== undefined ? this.datasetApplication : { name: '' },
            availableApplications: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableApplications();
        }
    },
    methods: {
        getAvailableApplications() {
            Promise.resolve(this.$root.getApiData(`/applications/all`))
            .then((apiData) => {
                this.availableApplications = apiData;
            });
        },
        selectDatasetApplication(datasetApplication) {
            this.data = datasetApplication;
        },
        applicationDetailsPageUrl() {
            return `/applications/details/${this.datasetApplication.id}`;
        }
    }
}
