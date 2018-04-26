/* Components */
import DatasetField from './DatasetFields/DatasetField.vue';

export default {
    name: 'DatasetFields',
    props: {
        datasetFields: Array,
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        DatasetField
    },
    data() {
        return {
            apiData: null,
            newDatasetField: false,
        }
    },
    watch: {
        datasetFields() {
            this.apiData = this.datasetFields;
        }
    },
    methods: {
        getDatasetFields() {
            Promise.resolve(this.$root.getApiData(`/datasets/${this.datasetId}/fields`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        createNewDatasetField() {
            this.newDatasetField = true;
        },
        removeNewDatasetField() {
            this.newDatasetField = false;
        },
        postDatasetField(data) {
            Promise.resolve(this.$root.postApiData(`/datasets/${this.datasetId}/fields`, data))
                .then(() => {
                    this.getDatasetFields();
                    this.removeNewDatasetField();
                });
        },
        updateDatasetField(datasetFieldId, data) {
            Promise.resolve(this.$root.putApiData(`/datasets/${this.datasetId}/fields`, data));
        },
        removeDatasetField(datasetFieldId) {
            Promise.resolve(this.$root.deleteApiData(`/datasets/${this.datasetId}/fields/${datasetFieldId}`))
                .then(() => {
                    this.getDatasetFields();
                });
        }
    }
}

