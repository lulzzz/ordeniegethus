/* Components */
import ResourceLink from './ResourceLinks/ResourceLink.vue';

export default {
    name: 'ResourceLinks',
    props: ['applicationId'],
    components: {
        ResourceLink
    },
    data() {
        return {
            apiData: null,
            newResourceLink: false,
            apiUrl: "/ResourceLinks/Application/" + this.applicationId
        }
    },
    mounted() {
        this.getResourceLinks();
    },
    methods: {
        getResourceLinks() {
            Promise.resolve(this.$root.getApiData(this.apiUrl))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        createNewResourceLink() {
            this.newResourceLink = true;
        },
        removeNewResourceLink() {
            this.newResourceLink = false;
        },
        postResourceLink(data) {
            Promise.resolve(this.$root.postApiData(this.apiUrl, data))
                .then(() => {
                    this.getResourceLinks();
                    this.removeNewResourceLink();
                });
        },
        updateResourceLink(resourceLinkId, data) {
            Promise.resolve(this.$root.putApiData(`${this.apiUrl}/${resourceLinkId}`, data));
        },
        removeResourceLink(resourceLinkId) {
            Promise.resolve(this.$root.postApiData('/ResourceLinks/Delete', { id: resourceLinkId }))
                .then(() => {
                    this.getResourceLinks();
                });
        }
    }
}

