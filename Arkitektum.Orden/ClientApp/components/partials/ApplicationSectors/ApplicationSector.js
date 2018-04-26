/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationSector',
    props: {
        applicationSector: Object,
        availableSectors: Array,
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
            data: this.applicationSector !== undefined ? this.applicationSector : { sectorName: '' }
        }
    },
    methods: {
        selectApplicationSector(applicationSector) {
            this.data = applicationSector;
        },
        sectorDetailsPageUrl() {
            return `/applications?sectorId=${this.applicationSector.id}`;
        }
    }
}
