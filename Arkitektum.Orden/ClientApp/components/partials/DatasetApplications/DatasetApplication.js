/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'DatasetApplication',
    props: {
        datasetApplication: Object,
        availableApplications: Array,
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
            data: this.datasetApplication !== undefined ? this.datasetApplication : { name: '' }
        }
    },
    methods: {
        selectDatasetApplication(datasetApplication) {
            this.data = datasetApplication;
        },
        applicationDetailsPageUrl() {
            return `/applications/details/${this.datasetApplication.id}`;
        }
    }
}
