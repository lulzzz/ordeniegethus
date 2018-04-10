/* Components */
import ResourceLink from './ResourceLinks/ResourceLink.vue';

export default {
    name: 'ResourceLinks',
    props: ['apiUrl'],
    components: {
        ResourceLink
    },
    data() {
        return {
            savedResourceLinks: null,
            newResourceLink: false
        }
    },
    mounted() {
        this.getResourceLinks();
    },
    methods: {
        getResourceLinks() {
            Promise.resolve(this.$root.getApiData(this.apiUrl))
                .then((apiData) => {
                    this.savedResourceLinks = apiData;
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

