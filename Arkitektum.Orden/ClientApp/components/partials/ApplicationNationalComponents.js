/* Components */
import ApplicationNationalComponent from './ApplicationNationalComponents/ApplicationNationalComponent.vue';

export default {
    name: 'ApplicationNationalComponents',
    props: {
        applicationNationalComponents: Array,
        applicationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ApplicationNationalComponent
    },
    data() {
        return {
            apiData: null,            
            newApplicationNationalComponent: false,
            availableNationalComponents: []
        }
    },
    mounted() {
        this.apiData = this.applicationNationalComponents;
    },
    watch: {
        apiData() {
            if (this.apiData) {
                this.getAvailableNationalComponents();
            }
        }
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
        getAvailableNationalComponents() {
            let availableNationalComponents = [];
            Promise.resolve(this.$root.getApiData(`/nationalcomponents/all`))
            .then((apiData) => {
                apiData.forEach(nationalComponent => {
                    if (!this.apiData.filter(nC => nC.id == nationalComponent.id).length) {
                        availableNationalComponents.push(nationalComponent);
                    }
                });
                this.availableNationalComponents = availableNationalComponents;
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

