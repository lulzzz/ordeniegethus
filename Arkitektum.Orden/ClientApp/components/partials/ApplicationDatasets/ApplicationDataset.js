/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationDataset',
    props: ['applicationDataset', 'selectedApplicationDatasets', 'applicationId', 'saved'],
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationDataset !== undefined ? this.applicationDataset : { datasetName: '' },
            availableDatasets: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableDatasets();
        }
    },
    methods: {
        getAvailableDatasets() {
            Promise.resolve(this.$root.getApiData(`/datasets/all`))
            .then((apiData) => {
                this.availableDatasets = apiData;
            });
        },
        selectApplicationDataset(applicationDataset) {
            this.data = applicationDataset;
        }
    }
}
