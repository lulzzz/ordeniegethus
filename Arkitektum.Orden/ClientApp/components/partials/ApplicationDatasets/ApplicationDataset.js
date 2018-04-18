/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationDataset',
    props: ['applicationDataset', 'applicationId', 'saved'],
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationDataset !== undefined ? this.applicationDataset : { datasetName: '' },
            availableApplicationDatasets: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableApplicationDatasets();
        }
    },
    methods: {
        getAvailableApplicationDatasets() {
            Promise.resolve(this.$root.getApiData(`/datasets/all`))
            .then((apiData) => {
                this.availableApplicationDatasets = apiData;
            });
        },
        selectApplicationDataset(applicationDataset) {
            this.data = applicationDataset;
        }
    }
}
