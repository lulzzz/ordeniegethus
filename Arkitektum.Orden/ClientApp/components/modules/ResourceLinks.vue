<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved resource links -->
            <resource-link v-bind:api-url="apiUrl" v-bind:resource-link="resourceLink" v-bind:saved="true" v-for="resourceLink in savedResourceLinks"></resource-link>

            <!-- New resource links -->
            <resource-link v-if="newResourceLink" v-bind:api-url="apiUrl" v-bind:saved="false" v-on:remove="removeNewResourceLink()"></resource-link>
        </div>
        <div class="input-group">
            <button v-show="!newResourceLink" v-on:click="createNewResourceLink()" class="float-right btn btn-primary">Ny lenke</button>
        </div>

    </div>
</template>

<script src="./ResourceLinks.js"></script>
<style src="./ResourceLinks.scss"></style>
