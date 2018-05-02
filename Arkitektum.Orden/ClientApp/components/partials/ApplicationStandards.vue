<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application standards -->
            <application-standard v-bind:application-standard="applicationStandard" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeApplicationStandard(applicationStandard.id)" v-for="applicationStandard in apiData" :key="applicationStandard.id"></application-standard>

            <!-- New application standards -->
            <application-standard v-if="newApplicationStandard" v-bind:available-standards="availableStandards" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationStandard()"></application-standard>

            <button v-if="writeAccess" v-show="!newApplicationStandard && availableStandards.length" v-on:click="createNewApplicationStandard()" class="float-right btn btn-outline-primary list-group-item-button"><span class="fas fa-plus"></span> Legg til standard</button>
        </div>
    </div>
</template>

<script src="./ApplicationStandards.js"></script>
<style src="./ApplicationStandards.scss"></style>
