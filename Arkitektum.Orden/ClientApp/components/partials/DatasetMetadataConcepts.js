import DatasetMetadataConcept from './DatasetMetadataConcepts/DatasetMetadataConcept.vue';

export default {
    name: 'DatasetMetadataConcepts',

    props: {
        datasetMetadataConcepts: Array,
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },

    components: {
        DatasetMetadataConcept
    },

    data() {
        return {
            apiData: null,
            availableConcepts: [],
            newDatasetMetadataConcept: false
        }
    },

    mounted() {
        this.apiData = this.datasetMetadataConcepts;
    },

    methods: {
        removeDatasetMetadataConcept(id) {

        },

        removeNewDatasetMetadataConcept(id) {

        },

        createNewDatasetMetadataConcept() {

        },

        postDatasetMetadataConcept(data) {

        }
    }



}