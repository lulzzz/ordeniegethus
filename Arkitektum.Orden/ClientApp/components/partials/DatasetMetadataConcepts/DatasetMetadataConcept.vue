<template>

  <!-- Saved concept -->
  <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
    {{ datasetMetadataConcept.name}}
    <div class="float-right" v-if="writeAccess">
      <span class="btn btn-outline-danger" v-on:click="$emit('remove')">
        <span class="fas fa-trash"></span>
      </span>
    </div>
  </div>

  <!-- New concept -->
  <div v-else="">
    <filter-select
      v-bind:options="availableApplications"
      v-bind:filterable-properties="['name']"
      primary-text-property="name"
      secondary-text-property="id"
      value-property="id"
      placeholder="Søk etter applikasjon"
      v-on:select="selectDatasetApplication($event)"
      class="list-group-item-input-field">
    </filter-select>
    <div class="row no-gutters">
      <div class="col-sm">
        <span class="btn btn-outline-danger list-group-item-button" v-on:click="$emit('remove')">
          <span class="fas fa-trash"></span> Fjern
        </span>
      </div>
      <div class="col-sm">
        <span class="btn btn-outline-success list-group-item-button" v-on:click="$parent.postDatasetApplication(data)">
          <span class="fas fa-check"></span> Legg til
        </span>
      </div>
    </div>
  </div>

</template>

<style src="./DatasetMetadataConcept.scss"></style>
<script src="./DatasetMetadataConcept"></script>
