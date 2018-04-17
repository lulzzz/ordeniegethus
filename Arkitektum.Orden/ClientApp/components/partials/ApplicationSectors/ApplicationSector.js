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
            Promise.resolve(this.$root.getApiData(`/sectors/all`))
            .then((apiData) => {
                this.availableApplicationSectors = apiData;
            });
        },
        selectApplicationSector(applicationSector) {
            this.data = applicationSector;
        }
    }
}
