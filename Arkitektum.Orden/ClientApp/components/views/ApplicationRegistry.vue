<template>
  <div>
    <h1>Registrer ny applikasjon</h1>

    <div class="form-group w-50">
      <filter-select
        v-bind:options="availableApplications"
        v-bind:selected-options="selectedApplication"
        v-bind:filterable-properties="['name']"
        primary-text-property="name"
        value-property="id"
        placeholder="Søk etter applikasjon"
        v-on:select="selectApplication($event)"
        v-on:user-input-change="updateApplicationQuery($event)">
      </filter-select>
    </div>
    <div class="form-group">
      <button class="btn btn-outline-success" v-on:click="createApplication()">
        <span class="fas fa-check"></span> Bruk denne oppføringen
      </button>

    </div>
    
    <div class="form-group">
      Fant du ikke det du lette etter? <a v-bind:href="'/applications/create?applicationQuery=' + applicationQuery">Registrer ny applikasjon her</a>
    </div>
  </div>

</template>

<script src="./ApplicationRegistry.js"></script>
<style src="./ApplicationRegistry.scss"></style>