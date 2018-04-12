/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationSector',
    props: ['applicationSector', 'applicationId', 'saved'],
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationSector !== undefined ? this.applicationSector : { sectorName: '' },
            apiUrls: {
                get: `/sectors/application/${this.applicationId}`
            },
            availableApplicationSectors: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableApplicationSectors();
        }
    },
    methods: {
        getAvailableApplicationSectors() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
            .then((apiData) => {
                this.availableApplicationSectors = apiData;
            });
        },
        selectApplicationSector(applicationSector) {
            this.data = applicationSector;
        }
    }
}
