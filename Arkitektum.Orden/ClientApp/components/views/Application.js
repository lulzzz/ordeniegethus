/* Components */
import ResourceLinks from '../partials/ResourceLinks.vue';
import ApplicationSuperUsers from '../partials/ApplicationSuperUsers.vue';
import ApplicationSectors from '../partials/ApplicationSectors.vue';
import ApplicationDatasets from '../partials/ApplicationDatasets.vue';
import ApplicationNationalComponents from '../partials/ApplicationNationalComponents.vue';
import ApplicationStandards from '../partials/ApplicationStandards.vue';
import { format } from 'date-fns';


export default {
    name: 'Application',
    props: {
        applicationId: String,
        organizationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    }, 
    data() {
        return {
            apiUrls: {
                get: `/api/applications/${this.applicationId}`,
                edit: `/applications/edit/${this.applicationId}`,
                submitAppRegistry: `/applications/submit-app-registry/${this.applicationId}`,
            },
            apiData: null
        }
    },
    components: {
        ResourceLinks,
        ApplicationSuperUsers,
        ApplicationSectors,
        ApplicationDatasets,
        ApplicationNationalComponents,
        ApplicationStandards
    },
    mounted() {
        this.getApiData();
    },
    methods: {
        getApiData() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        hasAgreementDetails() {
            if (this.apiData && 
                (this.apiData.agreementDateStart || this.apiData.agreementDescription || this.apiData.agreementTerminationClauses || this.apiData.agreementResponsibleRole || this.apiData.agreementDocumentUrl ))
                return true;
            return false;
        }
    },
    filters: {
        formatDateTime: function (value) {
            if (!value) return ''
            return format(value, 'D. MMMM YYYY HH:mm:ss')
        },
        formatDate: function (value) {
            if (!value) return ''
            return format(value, 'D. MMMM YYYY')
        }
    }
}

