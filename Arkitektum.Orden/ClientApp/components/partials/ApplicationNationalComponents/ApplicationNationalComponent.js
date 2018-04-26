/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationNationalComponent',
    props: {
        applicationNationalComponent: Object,
        availableNationalComponents: Array,
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
            data: this.applicationNationalComponent !== undefined ? this.applicationNationalComponent : { name: '' }
        }
    },
    methods: {
        selectApplicationNationalComponent(applicationNationalComponent) {
            this.data = applicationNationalComponent;
        },
        addApplicationNationalComponentToApplication() {
            this.$parent.postApplicationNationalComponent({ applicationId: this.applicationId, nationalComponentId: this.data.id });
        }        
    }
}
