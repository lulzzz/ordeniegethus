<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved dataset applications -->
            <dataset-application v-bind:dataset-application="datasetApplication" v-bind:write-access="writeAccess" v-bind:saved="true" 
                                 v-on:remove="removeDatasetApplication(datasetApplication.id)"
                                 v-for="datasetApplication in apiData" :key="datasetApplication.id">

            </dataset-application>

            <!-- New dataset applications -->
            <dataset-application v-if="newDatasetApplication" v-bind:available-applications="availableApplications" 
                                 v-bind:dataset-id="datasetId" v-bind:saved="false" v-on:remove="removeNewDatasetApplication()">

            </dataset-application>

            <button v-if="writeAccess" v-show="!newDatasetApplication && availableApplications.length"
                    v-on:click="createNewDatasetApplication()" 
                    class="float-right btn btn-outline-primary list-group-item-button"><span class="fas fa-plus"></span> Legg til applikasjon
             </button>
        </div>
    </div>
</template>

<script src="./DatasetApplications.js"></script>
<style src="./DatasetApplications.scss"></style>
