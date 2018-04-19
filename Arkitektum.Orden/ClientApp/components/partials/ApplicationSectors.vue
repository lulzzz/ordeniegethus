<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application sectors -->
            <application-sector v-bind:application-sector="applicationSector" v-bind:saved="true" v-on:remove="removeApplicationSector(applicationSector.sectorId)" v-for="applicationSector in apiData" :key="applicationSector.sectorId"></application-sector>

            <!-- New application sectors -->
            <application-sector v-if="newApplicationSector" v-bind:selected-application-sectors="apiData" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationSector()"></application-sector>

            <div class="list-group-item">
                <button v-show="!newApplicationSector" v-on:click="createNewApplicationSector()" class="float-right btn btn-primary"><span class="fas fa-plus"></span> Legg til tjenesteområde</button>
            </div>
        </div>
    </div>
</template>

<script src="./ApplicationSectors.js"></script>
<style src="./ApplicationSectors.scss"></style>
