/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'DatasetMetadataConcept',
    props: {
        datasetMetadataConcept: Object,
        availableConcepts: Array,
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
            data: this.datasetMetadataConcept !== undefined ? this.datasetMetadataConcept : { name: '' }
        }
    },
    methods: {
        selectDatasetMetadataConcept(datasetMetadataConcept) {
            this.data = datasetMetadataConcept;
        }
    }
}
