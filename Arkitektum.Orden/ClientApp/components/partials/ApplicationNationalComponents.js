/* Components */
import ApplicationNationalComponent from './ApplicationNationalComponents/ApplicationNationalComponent.vue';

export default {
    name: 'ApplicationNationalComponents',
    props: ['applicationNationalComponents', 'applicationId', 'availableNationalComponents'],
    components: {
        ApplicationNationalComponent
    },
    data() {
        return {
            apiData: null,            
            newApplicationNationalComponent: false,
        }
    },
    mounted() {
        this.apiData = this.applicationNationalComponents;
    },
    methods: {
        createNewApplicationNationalComponent() {
            this.newApplicationNationalComponent = true;
        },
        removeNewApplicationNationalComponent() {
            this.newApplicationNationalComponent = false;
        },
        getApplicationNationalComponents() {
            Promise.resolve(this.$root.getApiData(`/nationalcomponents/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        postApplicationNationalComponent(data) {
            Promise.resolve(this.$root.postApiData(`/nationalcomponents/application/`, data))
                .then(() => {
                    this.getApplicationNationalComponents();
                    this.removeNewApplicationNationalComponent();
                });
        },
        removeApplicationNationalComponent(applicationNationalComponentId) {
            Promise.resolve(this.$root.deleteApiData(`/nationalcomponents/application/${applicationNationalComponentId}/${this.applicationId}`))
                .then(() => {
                    this.getApplicationNationalComponents();
                });
        }
    }
}

