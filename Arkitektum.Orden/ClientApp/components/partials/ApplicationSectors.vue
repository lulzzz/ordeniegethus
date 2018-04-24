<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application sectors -->
            <application-sector v-bind:application-sector="applicationSector" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeApplicationSector(applicationSector.id)" v-for="applicationSector in apiData" :key="applicationSector.id"></application-sector>

            <!-- New application sectors -->
            <application-sector v-if="newApplicationSector" v-bind:selected-application-sectors="apiData" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationSector()"></application-sector>

            <div class="list-group-item" v-if="writeAccess">
                <button v-show="!newApplicationSector" v-on:click="createNewApplicationSector()" class="float-right btn btn-outline-primary"><span class="fas fa-plus"></span> Legg til tjenesteområde</button>
            </div>
        </div>
    </div>
</template>

<script src="./ApplicationSectors.js"></script>
<style src="./ApplicationSectors.scss"></style>
