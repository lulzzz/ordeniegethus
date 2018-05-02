/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationStandard',
    props: {
        applicationStandard: Object,
        availableStandards: Array,
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
            data: this.applicationStandard !== undefined ? this.applicationStandard : { name: '' }
        }
    },
    methods: {
        selectApplicationStandard(applicationStandard) {
            this.data = applicationStandard;
        },
        addApplicationStandardToApplication() {
            this.$parent.postApplicationStandard({ applicationId: this.applicationId, StandardId: this.data.id });
        }        
    }
}
