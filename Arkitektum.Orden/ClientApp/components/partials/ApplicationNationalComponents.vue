<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application national components -->
            <application-national-component v-bind:application-national-component="applicationNationalComponent" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeApplicationNationalComponent(applicationNationalComponent.id)" v-for="applicationNationalComponent in apiData" :key="applicationNationalComponent.id"></application-national-component>

            <!-- New application national components -->
            <application-national-component v-if="newApplicationNationalComponent" v-bind:available-national-components="availableNationalComponents" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationNationalComponent()"></application-national-component>

            <button v-if="writeAccess" v-show="!newApplicationNationalComponent && availableNationalComponents.length" v-on:click="createNewApplicationNationalComponent()" class="float-right btn btn-outline-primary list-group-item-button"><span class="fas fa-plus"></span> Legg til nasjonal felleskomponent</button>
        </div>
    </div>
</template>

<script src="./ApplicationNationalComponents.js"></script>
<style src="./ApplicationNationalComponents.scss"></style>
