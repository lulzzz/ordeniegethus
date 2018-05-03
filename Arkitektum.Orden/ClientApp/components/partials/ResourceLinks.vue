<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved resource links -->
            <resource-link v-bind:resource-link="resourceLink" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeResourceLink(resourceLink.id)" v-for="resourceLink in apiData" :key="resourceLink.id"></resource-link>

            <!-- New resource links -->
            <resource-link v-if="newResourceLink" v-bind:saved="false" v-on:remove="removeNewResourceLink()"></resource-link>

            <div class="list-group-item" v-if="writeAccess">
                <button v-show="!newResourceLink" v-on:click="createNewResourceLink()" class="float-right btn btn-outline-primary"><span class="fas fa-plus"></span> Ny lenke</button>
            </div>
        </div>
    </div>
</template>

<script src="./ResourceLinks.js"></script>
<style src="./ResourceLinks.scss"></style>
