/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationDataset',
    props: {
        applicationDataset: Object,
        availableDatasets: Array,
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
            data: this.applicationDataset !== undefined ? this.applicationDataset : { name: '' }
        }
    },
    methods: {
        selectApplicationDataset(applicationDataset) {
            this.data = applicationDataset;
        },
        datasetDetailsPageUrl() {
            return `/datasets/details/${this.applicationDataset.id}`;
        }
    }
}
