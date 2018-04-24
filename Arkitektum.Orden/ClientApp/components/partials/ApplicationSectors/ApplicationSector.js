/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationSector',
    props: {
        applicationSector: Object,
        selectedApplicationSectors: Array,
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
            data: this.applicationSector !== undefined ? this.applicationSector : { sectorName: '' },
            availableSectors: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableSectors();
        }
    },
    methods: {
        getAvailableSectors() {
            Promise.resolve(this.$root.getApiData(`/sectors/all`))
            .then((apiData) => {
                this.availableSectors = apiData;
            });
        },
        selectApplicationSector(applicationSector) {
            this.data = applicationSector;
        },
        sectorDetailsPageUrl() {
            return `/applications?sectorId=${this.applicationSector.id}`;
        }
    }
}
