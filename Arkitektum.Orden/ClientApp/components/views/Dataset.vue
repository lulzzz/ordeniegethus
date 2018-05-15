<template>
  <div v-if="apiData">
   
    <div class="row">
      <div class="col-md-3 border-right">
        <sidebar-navigation 
        v-bind:navigation-items="navigationItems"></sidebar-navigation>
      </div>
      <div class="col-md-9">
         <h1>{{ apiData.name }}</h1>
        <div class="btn-group" v-if="writeAccess">
          <a v-bind:href="apiUrls.edit" class="btn btn-sm btn-outline-primary">
            <i class="fas fa-edit"></i>
            Rediger
          </a>
        </div>

        <div id="details" class="section">
          <p v-if="apiData.userModified">
            Sist endret av {{ apiData.userModified }} - {{ apiData.dateModified |
            formatDate }}
          </p>
          <p v-if="apiData.userCreated">
            Opprettet av: {{ apiData.userCreated }} - {{ apiData.dateCreated |
            formatDate }}
          </p>

          <p>
            Inneholder personopplysninger: <strong>{{ apiData.hasPersonalData | formatBoolean }}</strong>
          </p>
          <p>
            Inneholder sensitive personopplysninger: <strong>{{ apiData.hasSensitivePersonalData | formatBoolean }}</strong>
          </p>
          <p>
            Inneholder master data: <strong>{{ apiData.hasMasterData | formatBoolean }}</strong>
          </p>
        </div>

        <div id="description" class="section">
          <h2>Beskrivelse</h2>
          <p v-if="apiData.description" class="long-text">{{ apiData.description }}</p>
          <p v-if="!apiData.description">
            <i>Ingen beskrivelse angitt.</i>
          </p>
        </div>
  
        <div id="purpose" class="section">
          <h2>Formål</h2>
          <p v-if="apiData.purpose" class="long-text">{{ apiData.purpose }}</p>
          <p v-if="!apiData.purpose">
            <i>Ingen formål angitt.</i>
          </p>
        </div>
       
        <div id="hosting" class="section">
          <h2>Drift</h2>
          <p v-if="apiData.hostingLocationText">Driftsplassering: {{ apiData.hostingLocationText }}</p>
        </div>

        <div id="details" class="section">
          <h2 id="fields">Informasjonselementer</h2>
          <dataset-fields v-bind:dataset-id="datasetId" v-bind:dataset-fields="apiData.fields" v-bind:write-access="writeAccess"></dataset-fields>
        </div>

        <div id="details" class="section">
          <h2 id="metadata">Metadata</h2>
          <dataset-metadata v-bind:write-access="writeAccess" v-bind:dataset-id="datasetId" v-bind:lists="metadataLists"></dataset-metadata>
        </div>

        <div id="resource-links" class="section">
          <h2>Lenker</h2>
          <resource-links v-bind:parent-id="datasetId" api-path="/api/ResourceLinks/Dataset" v-bind:write-access="writeAccess"></resource-links>
        </div>

        <div id="applications">
          <h2>Applikasjoner</h2>
          <dataset-applications v-bind:dataset-id="datasetId" v-bind:dataset-applications="apiData.applications" v-bind:write-access="writeAccess"></dataset-applications>
        </div>

      </div>
    </div>
  </div>

</template>

<script src="./Dataset.js"></script>
<style src="./Dataset.scss"></style>
