<template>
  <a-modal :title="title" :width="800" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
             <a-col :md="mdSize" :sm="smSize">
                 <a-form-model-item label="父级部门id" prop="parentId" hasFeedback>
                    <a-input v-model="form.parentId" placeholder="" />
                 </a-form-model-item>
             </a-col>
        </a-row>
        <a-row :gutter="gutter">
             <a-col :md="mdSize" :sm="smSize">
                 <a-form-model-item label="部门名字" prop="name" hasFeedback>
                      <a-select v-model="form.name">
                         <a-select-option v-for="(item, index) in MenuTypeData" :key="index" :value="item.value">
                              {{ item.name }}
                         </a-select-option>
                      </a-select>
                 </a-form-model-item>
             </a-col>
        </a-row>
        <a-row :gutter="gutter">
             <a-col :md="mdSize" :sm="smSize">
                 <a-form-model-item label="部门描述" prop="describe" hasFeedback>
                    <a-input v-model="form.describe" placeholder="" />
                 </a-form-model-item>
             </a-col>
        </a-row>
        <a-row :gutter="gutter">
             <a-col :md="mdSize" :sm="smSize">
                 <a-form-model-item label="部门启用" prop="enable" hasFeedback>
                     <a-select v-model="form.enable">
                         <a-select-option :value="true">
                             启用
                         </a-select-option>
                         <a-select-option :value="false">
                             禁用
                         </a-select-option>
                     </a-select>
                 </a-form-model-item>
             </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { sysDict } from '@/api/system'
export default {
  data() {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 48,
      mdSize: 24,
      smSize: 24,
      MenuTypeData:[],
      formType: 'add',
      title: '添加数据',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      rules: {
          parentId:[{ required: true,message: '请输入父级部门id！'}],
          name:[{ required: true,message: '请输入部门名字！'}],
          describe:[{ required: true,message: '请输入部门描述！'}],
          enable:[{ required: true,message: '请输入部门启用！'}],
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail(value) {
      this.visible = true
      this.confirmLoading = false
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      this.dromdownList()
      if (value) {
        this.form = JSON.parse(JSON.stringify(value))
        this.formType = 'edit'
        this.title = '编辑数据'
      }
      else {
        this.title = '新建数据'
        this.formType = 'add'
        this.form = createForm()
      }
    },
    dromdownList() {
      sysDict({ code: 'MenuType' }).then((res) => {
        if (res.code === 200) {
          this.MenuTypeData = res.result
        } else {
         this.$message.warning(res.message)
        }
      })
    },
    async handleOk() {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.confirmLoading = true    
          // 修改数据
          if (this.formType === 'edit') {
            await this.$http.put('/sysDept', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(err => {
              this.confirmLoading = false
            })
          } else {
            // 保存数据
            // 先上传文件
            await this.$http.post('/sysDept', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(err => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    handleCancel() {
      this.close()
    },
    close() {
      this.$emit('close')
      this.visible = false
    }
  }
}
function createForm() {
  return {
   
  }
}
</script>