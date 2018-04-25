/* Components */
//import ResourceLink from './DatasetFields/DatasetField.vue';

export default {
    name: 'DatasetFields',
    props: {
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
  //      ResourceLink
    },
    data() {
        return {
            apiData: null,
            newDatasetField: false,
        }
    },
    mounted() {
        this.getDatasetFields();
    },
    methods: {
        getDatasetFields() {
            Promise.resolve(this.$root.getApiData(`/datasets/${this.datasetId}/fields`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        createNewDatasetField() {
            this.newDatsetField = true;
        },
        removeNewDatasetField() {
            this.newDataset = false;
        },
        postDatasetField(data) {
            Promise.resolve(this.$root.postApiData(`/datasets/${this.datasetId}/fields`, data))
                .then(() => {
                    this.getDatasetFields();
                    this.removeNewDatasetField();
                });
        },
        updateDatasetField(datasetFieldId, data) {
            Promise.resolve(this.$root.putApiData(`/datasets/${this.datasetId}/fields/${datasetFieldId}`, data));
        },
        removeResourceLink(datasetFieldId) {
            Promise.resolve(this.$root.deleteApiData(`/datasets/${this.datasetId}/fields/${datasetFieldId}`))
                .then(() => {
                    this.getDatasetFields();
                });
        }
    }
}

