import DatasetMetadataConcept from './DatasetMetadataConcepts/DatasetMetadataConcept.vue';

export default {
    name: 'datasetMetadataConcepts',

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



    methods: {
        removeDatasetMetadataConcept(id) {

        }
    }



}