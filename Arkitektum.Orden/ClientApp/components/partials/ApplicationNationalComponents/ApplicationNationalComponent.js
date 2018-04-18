/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationNationalComponent',
    props: ['applicationNationalComponent', 'applicationId', 'saved'],
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationNationalComponent !== undefined ? this.applicationNationalComponent : { nationalComponentName: '' },
            availableApplicationNationalComponents: []
        }
    },
    mounted() {
        if (!this.saved){
            this.getAvailableApplicationNationalComponents();
        }
    },
    methods: {
        getAvailableApplicationNationalComponents() {
            Promise.resolve(this.$root.getApiData(`/national-components/all`))
            .then((apiData) => {
                this.availableApplicationSectors = apiData;
            });
        },
        selectApplicationNationalComponent(applicationNationalComponent) {
            this.data = applicationNationalComponent;
        }
    }
}
