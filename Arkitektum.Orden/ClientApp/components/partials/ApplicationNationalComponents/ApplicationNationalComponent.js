/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationNationalComponent',
    props: ['applicationNationalComponent', 'selectedApplicationNationalComponents', 'applicationId', 'saved'],
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationNationalComponent !== undefined ? this.applicationNationalComponent : { name: '' },
            availableNationalComponents: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableNationalComponents();
        }
    },
    methods: {
        getAvailableNationalComponents() {
            Promise.resolve(this.$root.getApiData(`/nationalcomponents/all`))
            .then((apiData) => {
                this.availableNationalComponents = apiData;
            });
        },
        selectApplicationNationalComponent(applicationNationalComponent) {
            this.data = applicationNationalComponent;
        },
        addApplicationNationalComponentToApplication() {
            this.$parent.postApplicationNationalComponent({ applicationId: this.applicationId, nationalComponentId: this.data.id });
        }        
    }
}
