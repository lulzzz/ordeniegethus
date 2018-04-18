/* Components */
import ApplicationNationalComponent from './ApplicationNationalComponents/ApplicationNationalComponent.vue';

export default {
    name: 'ApplicationNationalComponents',
    props: ['applicationNationalComponents', 'applicationId', 'availableApplicationNationalComponents'],
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
            Promise.resolve(this.$root.getApiData(`/national-components/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        postApplicationNationalComponent(data) {
            Promise.resolve(this.$root.postApiData(`/national-components/application/`, data))
                .then(() => {
                    this.getApplicationNationalComponents();
                    this.removeNewApplicationNationalComponent();
                });
        },
        removeApplicationNationalComponent(applicationNationalComponentId) {
            Promise.resolve(this.$root.deleteApiData(`/national-components/application/`, { id: applicationNationalComponentId }))
                .then(() => {
                    this.getApplicationNationalComponents();
                });
        }
    }
}

