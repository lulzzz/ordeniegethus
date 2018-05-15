/* Components */
import SidebarNavigation from '../modules/SidebarNavigation.vue';
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
            apiData: null,
            navigationItems: [
                {
                    name: 'Detaljer',
                    id: 'details'
                },
                {
                    name: 'Drift',
                    id: 'hosting'
                },
                {
                    name: 'Metadata',
                    id: 'metadata'
                },
                {
                    name: 'Superbrukere',
                    id: 'super-users'
                },
                {
                    name: 'Lenker',
                    id: 'resource-links'
                },
                {
                    name: 'Tjenesteområder',
                    id: 'sectors'
                },
                {
                    name: 'Datasett',
                    id: 'dataset'
                },
                {
                    name: 'Nasjonale felleskomponenter',
                    id: 'national-components'
                },
                {
                    name: 'Standarder',
                    id: 'standards'
                }
            ]
        }
    },
    components: {
        SidebarNavigation,
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
        addConditionalNavigationItems() {
            if (this.hasAgreementDetails()){
                this.navigationItems.splice(2, 0, {name: 'Avtale', id: 'agreement'});
            }
            if (this.hasCostDetails()){
                this.navigationItems.splice(1, 0, {name: 'Kostnader', id: 'cost'});
            }
        },
        getApiData() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
                .then((apiData) => {
                    this.apiData = apiData;
                    this.addConditionalNavigationItems();
                });
        },
        hasAgreementDetails() {
            if (this.apiData && 
                (this.apiData.agreementDateStart || this.apiData.agreementDescription || this.apiData.agreementTerminationClauses || this.apiData.agreementResponsibleRole || this.apiData.agreementDocumentUrl ))
                return true;
            return false;
        },
        hasCostDetails() {
            return this.apiData.annualFee != 0 || this.apiData.initialCost != 0 || this.apiData.purchaseDate
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

