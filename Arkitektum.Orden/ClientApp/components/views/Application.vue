<template>
    <div v-if="apiData">
        <h1>{{ apiData.name }}  - versjon {{ apiData.version }}</h1>
        <hr/>
        <div class="row">
          <div class="col-lg-8">
              <div class="btn-group" v-if="writeAccess">
                <a v-bind:href="apiUrls.edit" class="btn btn-sm btn-outline-primary"><i class="fas fa-edit"></i> Rediger</a>
                <a v-bind:href="apiUrls.submitAppRegistry" class="btn btn-sm btn-outline-primary"><i class="fas fa-warehouse"></i> Send inn til felles applikasjonsregister</a>
              </div>
              
              <p v-if="apiData.userModified">Sist endret av {{ apiData.userModified }} - {{ apiData.dateModified | formatDateTime }}</p>
              <p v-if="apiData.userCreated">Opprettet av: {{ apiData.userCreated }} - {{ apiData.dateCreated | formatDateTime }}</p>
              <p>Produsent: {{ apiData.vendor.name }}</p>
              <p v-if="apiData.systemOwnerName">Systemansvarlig: <span class="badge badge-info"><i class="fas fa-user"></i> {{ apiData.systemOwnerName }}</span></p>

              <div v-if="apiData.annualFee != 0 || apiData.initialCost != 0 || apiData.purchaseDate ">
                  <h2>Kostnader</h2>
                  <hr />
                  <p v-if="apiData.annualFee != 0">Årlige kostnad: {{ apiData.annualFee }}</p>
                  <p v-if="apiData.initialCost != 0">Innkjøpskostnad: {{ apiData.initialCost }}</p>
                  <p v-if="apiData.purchaseDate">Anskaffelsesdato: {{ apiData.purchaseDate | formatDate }}</p>
              </div>
              
              <h2>Drift</h2>
              <hr />
              <p>Driftsleverandør: {{ apiData.hostingVendor }}</p>
              <p>Driftsplassering: {{ apiData.hostingLocationText }}</p>
              <p>Antall brukere: {{ apiData.numberOfUsers }}</p>
              
              <div v-if="hasAgreementDetails()">
                  <h2>Avtale</h2>
                  <hr/>
                  <p v-if="apiData.agreementDateStart">Dato inngått: {{ apiData.agreementDateStart | formatDate }}</p>
                  <p v-if="apiData.agreementDescription">Beskrivelse: {{ apiData.agreementDescription }}</p>
                  <p v-if="apiData.agreementTerminationClauses">Oppsigelsesbetingelser: {{ apiData.agreementTerminationClauses }}</p>
                  <p v-if="apiData.agreementResponsibleRole">Avtaleansvarlig (rolle): {{ apiData.agreementResponsibleRole }}</p>
                  <p v-if="apiData.agreementDocumentUrl"><a v-bind:href="apiData.agreementDocumentUrl">Lenke til avtale</a></p>
              </div>
<!--
              <p>
                Superbrukere:
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Hans Hansen
                </a>
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Jan Johansen
                </a>
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Ole Olsen
                </a>
              </p>
-->

                <h2>Superbrukere</h2>
                <hr/>
                <application-super-users v-bind:organization-id="organizationId" v-bind:application-id="applicationId" route="/api/applications" v-bind:write-access="writeAccess"></application-super-users>

                <h2>Lenker</h2>
                <hr />
                <resource-links v-bind:parent-id="applicationId" api-path="/ResourceLinks/Application" v-bind:write-access="writeAccess"></resource-links>

<!--                
                <h2>Avtale</h2>
                <hr />
                <p>Detaljer om avtale.</p>

                
                <h2>Vurderingskriterier</h2>
                <hr />
                <p>Brukertilfredshet:</p>
                <p>Applikasjonsmodenhet:</p>
                <p>Skalerbarhet:</p>
-->
            </div>
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header">Tjenesteområder</div>
                    <application-sectors v-bind:application-sectors="apiData.sectors" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-sectors>
                </div>
                <div class="mt-3"></div>
                <div class="card">
                    <div class="card-header">Datasett</div>
                    <application-datasets v-bind:application-datasets="apiData.datasets" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-datasets>
                </div>
                <div class="mt-3"></div>
                <div class="card">
                    <div class="card-header">Nasjonale felleskomponenter</div>
                    <application-national-components v-bind:application-national-components="apiData.nationalComponents" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-national-components>
                </div>
                <div class="mt-3"></div>
                <div class="card">
                    <div class="card-header">Standarder</div>
                    <application-standards v-bind:application-standards="apiData.standards" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-standards>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./Application.js"></script>
<style src="./Application.scss"></style>
