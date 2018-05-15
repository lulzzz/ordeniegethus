<template>
  <div>
    <div class="list-group list-group-flush">
      <!-- Saved concepts -->
      <dataset-metadata-concept v-bind:dataset-metadata-concept="datasetMetadataConcept" v-bind:write-access="writeAccess" v-bind:saved="true" 
                                v-on:remove="removeDatasetMetadataConcept(datasetMetadataConcept.code)" 
                                v-for="datasetMetadataConcept in apiData" :key="datasetApplication.code">

      </dataset-metadata-concept>

      <!-- New concepts -->
      <dataset-metadata-concept v-if="newDatasetMetadataConcept" v-bind:available-concepts="availableConcepts" 
                                v-bind:dataset-id="datasetId" v-bind:saved="false" 
                                v-on:remove="removeNewDatasetMetadataConcept(datasetMetdataConcept.code)">

      </dataset-metadata-concept>

      <button v-if="writeAccess" v-show="!newDatasetMetadataConcept && availableConcepts.length"
              v-on:click="createNewDatasetMetadataConcept()"
              class="float-right btn btn-outline-primary list-group-item-button">
        <span class="fas fa-plus"></span> Legg til tema
      </button>
    </div>
  </div>
</template>

<script src="./DatasetMetadataConcepts.js"></script>
<style src="./DatasetMetadataConcepts.scss"></style>